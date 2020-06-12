$(document).ready(function () {

	$('.cancelOrderForm').submit(function (event) {
		event.preventDefault();

		let $form = $(this);
		$.ajax({
			url: $form.attr('action'),
			type: $form.attr('method'),
			data: $form.serialize(),
			success: function (result) {
				if (result.errors.length > 0)
					showErrors(result.errors);
				else $form.closest("tr").remove();
			},
			error: function (result) {
				showErrors([$form.attr("error"), JSON.stringify(result)]);
			}
		});
	});
});