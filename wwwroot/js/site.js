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