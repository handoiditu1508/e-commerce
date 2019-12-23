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
});