$(document).ready(function () {

	//dropdown
	$('.btn-group').hover(
		function () {
			$('>.dropdown-menu', this).stop(true, true).fadeIn('fast');
			$(this).addClass('open');
		},
		function () {
			$('>.dropdown-menu', this).stop(true, true).fadeOut('fast');
			$(this).removeClass('open');
		});

	$('.dropdown-toggle, .dropdown-item').click(function () {
		$(this).next('ul').toggle();
	});

	//close errors zone
	$('#closeErrorZoneBtn').click(function (event) {
		event.preventDefault();
		$('#errorZone').addClass("d-none");
	});

	//close messages zone
	$('#closeMessageZoneBtn').click(function (event) {
		event.preventDefault();
		$('#messageZone').addClass("d-none");
	});
});

//show errors zone & error strings
function showErrors(errorsArray) {
	document.getElementById("errorContent").innerHTML = "<p>" + errorsArray.join("</p><p>") + "</p>";
	document.getElementById("errorZone").classList.remove("d-none");
}

//show messages zone & message strings
function showMessages(messagesArray) {
	document.getElementById("messageContent").innerHTML = "<p>" + errorsArray.join("</p><p>") + "</p>";
	document.getElementById("messageZone").classList.remove("d-none");
}