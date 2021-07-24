// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('.carousel').carousel({
    interval: 5000
})

// When the user scrolls the page, execute myFunction
window.onscroll = function() {addStricky()};

// Get the navbar
var navbar = document.getElementById("navbar");
var section = document.getElementById("section")

// Get the offset position of the navbar
var sticky = navbar.offsetTop;

function addStricky() {
  if (window.pageYOffset >= sticky) {
    navbar.classList.add("sticky")
    section.classList.add("padding")
  } else {
    navbar.classList.remove("sticky")
    section.classList.remove("padding")
  }
}

//Sezione per settare il PATH con l'option nell'index di elenco
var btnAlert = document.getElementById("btnAlert");
var btnAlertRegione = document.getElementById("btnAlertRegione");

$('#btnAddCity').click( function (e){
    e.preventDefault();
    
    var x = $('#citta').val();
    if(x != "-1") {
        btnAlert.classList.remove("show")
        window.location.href = $(this).attr('href') + '/' + x;
        
    }else{
        console.log("Errore!")
        btnAlert.classList.remove("d-none")
        btnAlert.classList.add("show")
    }
});

 $('#btnAddRegion').click( function (e){
    e.preventDefault();
    
    var x = $('#regione').val();
    if(x != "-1") {
        btnAlertRegione.classList.remove("show")
        window.location.href = $(this).attr('href') + '/' + x;
        
    }else{
        console.log("Errore!")
        btnAlertRegione.classList.remove("d-none")
        btnAlertRegione.classList.add("show")
    }
});




