$(document).ready(function () {
	//disable buttons
	function disableCartTableButtons() {
		$('.cartTableBtn').attr("disabled", true);
	}

	function enableCartTableButtons() {
		$('.cartTableBtn').removeAttr('disabled');
	}

	//reload cart table when remove product
	$('.cartTableContainer').on('submit', '.removeFromCartOnSubmit', function (event) {
		// Stop form from submitting normally
		event.preventDefault();
		disableCartTableButtons();

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
				enableCartTableButtons();
			}
		});
	});

	//reload cart table when change product quantity
	$('.cartTableContainer').on('submit', '.changeCartQuantityOnSubmit', function (event) {
		// Stop form from submitting normally
		event.preventDefault();
		disableCartTableButtons();

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
				enableCartTableButtons();
			}
		});
	});

	//remove disable form btn when quantity input changed
	$('.cartTableContainer').on('change', '.quantityInput', function () {
		let $submitBtn = $(this).closest('.changeCartQuantityOnSubmit').find('input[type=submit]');
		$submitBtn.removeAttr('disabled hidden');
	});

	//reload cart table when checkout
	$('.cartTableContainer').on('submit', '.checkoutOnSubmit', function (event) {
		// Stop form from submitting normally
		event.preventDefault();
		disableCartTableButtons();

		let $form = $(this);
		$.ajax({
			url: $form.attr('action'),
			type: $form.attr('method'),
			data: $form.serialize(),
			success: function (result) {
				if (result == null) {
					let customerLoginUrl = $('#customerLoginUrl').val();
					let currentUrl = window.location.href;
					customerLoginUrl += "?returnUrl=" + currentUrl;
					location.replace(customerLoginUrl);
				}
				else {
					updateSmallCart();
					$form.closest(".cartTableContainer").html(result);
				}
			},
			error: function (result) {
				showErrors(['Something went wrong while ordering item', JSON.stringify(result)]);
				enableCartTableButtons();
			}
		});
	});

	//reload cart table when checkout all
	$('.cartTableContainer').on('click', '.checkoutAllBtn', function (event) {
		event.preventDefault();
		disableCartTableButtons();

		let $btn = $(this);
		$.ajax({
			url: $btn.attr('urlAction'),
			type: $btn.attr('urlMethod'),
			success: function (result) {
				if (result == null) {
					let customerLoginUrl = $('#customerLoginUrl').val();
					let currentUrl = window.location.href;
					customerLoginUrl += "?returnUrl=" + currentUrl;
					location.replace(customerLoginUrl);
				}
				else {
					updateSmallCart();
					$btn.closest(".cartTableContainer").html(result);
				}
			},
			error: function (result) {
				showErrors(['Something went wrong while ordering all item', JSON.stringify(result)]);
				enableCartTableButtons();
			}
		});
	});
});