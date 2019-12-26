$(document).ready(function () {
	//small cart updating
	function updateSmallCart() {
		$.ajax({
			url: $('#cartTotalQuantityUrl').val(),
			type: $('#cartTotalQuantityUrl').attr("urlMethod"),
			success: function (result) {
				$('#cartQuantity').html(result);
			}
		});
	}
});