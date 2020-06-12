$(document).ready(function () {

	function isNullOrSpaces(str) {
		return str === null || str.match(/^ *$/) !== null;
	}

	//update attributes
	$('.updateAttributesForm').submit(function (event) {
		event.preventDefault();

		let attributes = {};

		let valuesArrays = [];

		$('.attributeValues').each(function (index, element) {
			valuesArrays.push($(this).tagsinput('items'));
		});

		let count = 0;
		$('.attributeKey').each(function (index, element) {
			if (!isNullOrSpaces($(this).val())) {
				attributes[$(this).val()] = valuesArrays[count];
			}
			count++;
		});

		if (!jQuery.isEmptyObject(attributes)) {
			let $form = $(this);
			let container = $form.closest(".updateProductAttributesContainer");
			$.ajax({
				url: $form.attr('action'),
				type: $form.attr('method'),
				data: {
					serializedUpdateViewModel: JSON.stringify({
						SellerId: container.find(".sellerId").val(),
						ProductTypeId: container.find('.productTypeId').val(),
						Attributes: attributes
					})
				},
				success: function (result) {
					if (typeof result !== 'undefined' && result.length > 0) {
						showErrors(result)
					}
					else location.reload();
				},
				error: function (result) {
					showErrors(['Something went wrong while updating attributes', JSON.stringify(result)]);
				}
			});
		}
	});
});