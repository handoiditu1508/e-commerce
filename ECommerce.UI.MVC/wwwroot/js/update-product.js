$(document).ready(function () {

	$(document).ready(function () {
		$('.imagesCheckbox').change(function () {

			let fileInput = $(this).closest(".imageUpdateContainer").find('.imagesFileInput');
			if (this.checked) {
				fileInput.fileinput('enable');
			}
			else fileInput.fileinput('disable');
		});

		$('.unregisterBtn').click(function (event) {
			event.preventDefault();
			if (!confirm("Warning: this action will delete this product parmanently"))
				return;

			showErrors(["Something went wrong while unregister product", "asdsd"]);
			$.ajax({
				url: $(this).attr("urlAction"),
				type: $(this).attr("urlMethod"),
				success: function (result) {
					if (result.errors.length < 1) {
						alert("Product successfully unregistered");
						location.replace(window.location.protocol + "//" + window.location.host + $(this).attr("redirectUrl"));
					}
					else showErrors(result.errors);
				},
				error: function (result) {
					showErrors(["Something went wrong while unregister product", JSON.stringify(result)]);
				}
			});
		});
	});
});