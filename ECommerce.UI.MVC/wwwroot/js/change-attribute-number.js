$(document).ready(function () {
	//change attribute number on click
	$('.changeAttributeNumberBtn').click(function (event) {
		event.preventDefault();

		let $container = $(this).closest('.changeAttributeNumber');

		let $value = $container.find('.changeAttributeNumberValue').val();

		let $link = $container.find('.changeAttributeNumberUrl').val();

		window.location.replace($link + '&productAttributesNumber=' + $value);
	});
});