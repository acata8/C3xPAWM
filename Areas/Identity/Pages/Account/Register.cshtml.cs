using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace C3xPAWM.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IOptionsMonitor<UsersOptions> _userOptions;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IOptionsMonitor<UsersOptions> userOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _userOptions = userOptions;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Nome obbligatorio")]
            [StringLength(100, MinimumLength = 3, ErrorMessage = "Il nome deve essere almeno {2} e massimo {1} caratteri.")]
            [Display(Name = "Nome completo")]
            public string FullName { get; set; }

            [Required(ErrorMessage = "Email obbligatoria")]
            [EmailAddress(ErrorMessage = "Formato mail non valido")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Ruolo obbligatorio")]
            [Display(Name = "Seleziona ruolo")]
            public string Ruolo {get; set;}

            

            [Required(ErrorMessage = "Password obbligatoria")]
            [StringLength(50, ErrorMessage = "Deve essere almeno {2} e massimo {1} caratteri.", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Conferma password")]
            [Compare("Password", ErrorMessage = "Password non coincidono")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, FullName = Input.FullName, Ruolo= Input.Ruolo, Proprietario=0, IdRuolo=0};
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    Claim baseClaim = new (ClaimTypes.Role, "Utente");
                    IdentityResult baseRoleAssignement = await _userManager.AddClaimAsync(user, baseClaim);
                    _logger.LogInformation("User created a new account with password. Role: UTENTE");

                    if(user.Email.Equals(_userOptions.CurrentValue.AssignAdministratorRoleOnRegistration, StringComparison.OrdinalIgnoreCase)){
                        Claim claim = new (ClaimTypes.Role, "Administrator");
                        IdentityResult roleAssignement = await _userManager.AddClaimAsync(user, claim);
                        if(!roleAssignement.Succeeded){
                            _logger.LogWarning("Ruolo di amministratore non assegnato");
                        }
                    }

                    string x = Input.Ruolo;
                    if(user.Ruolo.Equals(x) && !x.Equals("Utente")){
                        Claim claim = new (ClaimTypes.Role, x);
                        IdentityResult roleAssignement = await _userManager.AddClaimAsync(user, claim);
                        if(!roleAssignement.Succeeded){
                            _logger.LogWarning($"Ruolo di {x} non assegnato");
                        }
                        _logger.LogInformation($"Loggato come {x}");
                    }

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Conferma il tuo account",
                        $"Conferma il tuo account <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>cliccando qui</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        if(Input.Ruolo.Equals("Commerciante")){
                            return LocalRedirect("/Negozio/Creazione");
                        }
                        if(Input.Ruolo.Equals("Corriere")){
                            return LocalRedirect("/Corriere/Creazione");
                        }
                           
                        
                        return LocalRedirect(returnUrl);
                    }
                    
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            
            return Page();
        }
    }
}
