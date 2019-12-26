$(document).ready(function () {
	//reload cart table when remove product
	$('.cartTableContainer').on('submit', '.removeFromCartOnSubmit', function (event) {
		// Stop form from submitting normally
		event.preventDefault();
		let $form = $(this);

		$.ajax({
			url: $form.attr('action'),
			type: $form.attr('method'),
			data: $form.serialize(),
			success: function (result) {
				updateSmallCart();
				$form.closest(".cartTableContainer").html(result);
			},
			error: function (result) {
				showErrors(['Something went wrong while removing from cart', JSON.stringify(result)]);
			}
		});
	});

	//reload cart table when change product quantity
	$('.cartTableContainer').on('submit', '.changeCartQuantityOnSubmit', function (event) {
		// Stop form from submitting normally
		event.preventDefault();
		let $form = $(this);
		$.ajax({
			url: $form.attr('action'),
			type: $form.attr('method'),
			data: $form.serialize(),
			success: function (result) {
				updateSmallCart();
				$form.closest(".cartTableContainer").html(result);
			},
			error: function (result) {
				showErrors(['Something went wrong while changing cart quantity', JSON.stringify(result)]);
			}
		});
	});

	//remove disable form btn when quantity input changed
	$('.cartTableContainer').on('change', '.quantityInput', function () {
		let $submitBtn = $(this).closest('.changeCartQuantityOnSubmit').find('input[type=submit]');
		$submitBtn.removeAttr('disabled hidden');
	});
});