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

$("#success-alert").fadeTo(2000, 500).slideUp(500, function(){
  $("#success-alert").slideUp(500);
});

$("#error-alert").fadeTo(2000, 500).slideUp(500, function(){
  $("#error-alert").slideUp(500);
});

         
      