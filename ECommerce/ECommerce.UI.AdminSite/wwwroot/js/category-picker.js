$(document).ready(function () {
	//picking category

	$('.categoryPickingButton').click(function (event) {
		// Stop form from submitting normally
		event.preventDefault();
		let $btn = $(this);
		let $attr = $btn.find('p[name=\'categoryId\']');

		let container = $(this).closest(".categoryPickerContainer");
		container.find(".categoryHiddenPickingResult").val($attr.html());

		$.ajax({
			url: $('#getCategoryBreadCrumbUrl').val(),
			type: $('#getCategoryBreadCrumbUrl').attr("urlMethod"),
			data: { categoryId: $attr.html() },
			success: function (result) {
				container.find(".categoryPickingResult").html(result);
			},
			error: function (result) {
				showErrors(['Something went wrong while picking category', JSON.stringify(result)]);
			}
		});
	});

	$('.clearCategoryPicking').click(function(event) {
		// Stop btn behavior to make form validate it self
		event.preventDefault();

		//clear result of picking category
		let container = $(this).closest(".categoryPickerContainer");
		container.find(".categoryHiddenPickingResult").val('');
		container.find('.categoryPickingResult').html('');
	});
});