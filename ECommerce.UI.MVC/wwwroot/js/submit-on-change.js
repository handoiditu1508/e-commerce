$(document).ready(function () {

	$('.submitOnChange').change(function () {
		let $form = $(this).closest('form');
		let error = $(this).attr("error");
		$.ajax({
			url: $form.attr('action'),
			type: $form.attr('method'),
			data: $form.serialize(),
			success: function (result) {
				if (result.errors.length > 0)
					showErrors(result.errors);
			},
			error: function (result) {
				let array = [error, JSON.stringify(result)];
				showErrors(array);
			}
		});
	});
});