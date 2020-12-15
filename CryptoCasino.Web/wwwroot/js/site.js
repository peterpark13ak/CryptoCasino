// Write your JavaScript code.
/* Set the width of the side navigation to 250px and the left margin of the page content to 250px and add a black background color to body */
function openNav() {
	document.getElementById("mySidenav").style.width = "250px";
	document.getElementById("open-admin-nav").style.visibility = "hidden";
}

/* Set the width of the side navigation to 0 and the left margin of the page content to 0, and the background color of body to white */
function closeNav() {
	document.getElementById("mySidenav").style.width = "0";
	document.getElementById("open-admin-nav").style.visibility = "visible";
}
