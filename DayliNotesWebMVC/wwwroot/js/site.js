var panel = document.querySelector('.panel');

// Get the panel toggle button element
var toggle = document.querySelector('.panel-toggle');

// When the user clicks on the button, toggle the panel
toggle.onclick = function () {
    panel.classList.toggle('open');
}
