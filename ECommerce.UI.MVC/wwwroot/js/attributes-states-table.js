$(document).ready(function () {

	//add attributes state
	$('.AttributesStatesTableContainer').on('click', '.addAttributesStatesBtn', function (event) {

		let attributesState = {};
		$('.addAttributesStatesInput').each(function (index, element) {
			attributesState[$(this).attr('name')] = $(this).val();
		});

		let container = $(this).closest('.AttributesStatesTableContainer');
		$.ajax({
			url: $('#addAttributesStateUrl').val(),
			type: 'post',
			data: {
				productTypeId: container.find('.productTypeId').val(),
				attributesState: attributesState
			},
			success: function (result) {
				if (Array.isArray(result)) {
					showErrors(result);
				}
				else container.html(result);
			},
			error: function (result) {
				showErrors(['Something went wrong while adding attributes state', JSON.stringify(result)]);
			}
		});
	});

	//delete attributes state
	$('.AttributesStatesTableContainer').on('submit', '.deleteAttributesStateOnSubmit', function (event) {
		event.preventDefault();
		let $form = $(this);
		$.ajax({
			url: $form.attr('action'),
			type: $form.attr('method'),
			data: $form.serialize(),
			success: function (result) {
				if (Array.isArray(result)) {
					showErrors(result);
				}
				else $form.closest('.AttributesStatesTableContainer').html(result);
			},
			error: function (result) {
				showErrors(['Something went wrong while deleting attributes state', JSON.stringify(result)]);
			}
		});
	});
});