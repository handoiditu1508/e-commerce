$(document).ready(function () {

	//change product quantity
	$('.changeQuantityForm').submit(function (event) {
		event.preventDefault();
		let $form = $(this);

		let actionName = $form.attr("actionName");

		let changingNumbers = prompt("Please enter " + actionName + " numbers:", "0");

		if (changingNumbers == null) {
			alert("Empty value entered. Nothing changed");
			return;
		}

		if (isNaN(changingNumbers)) {
			alert("Numbers must be an integer");
			return;
		}

		let convertedChangingNumbers = Number(changingNumbers);
		if (convertedChangingNumbers != changingNumbers) {
			alert("Numbers must be an integer");
			return;
		}

		if (convertedChangingNumbers == 0)
			return;

		if (convertedChangingNumbers < 0) {
			alert("Numbers must be greater than zero");
			return;
		}

		$form.find("input[name='numbers']").val(convertedChangingNumbers);

		$.ajax({
			url: $form.attr('action'),
			type: $form.attr('method'),
			data: $form.serialize(),
			success: function (result) {
				if (result.errors.length < 1) {
					let sltor = ".product" + $form.find("input[name='productTypeId']").val() + "Quantity";
					$form.closest(".productQuantityCell").find(sltor).html(result.result);
				}
				else showErrors(result.errors);

				$form.find("input[name='numbers']").val(0);
			},
			error: function (result) {
				showErrors(['Something went wrong while ' + actionName + ' product quantity', JSON.stringify(result)]);
				$form.find("input[name='numbers']").val(0);
			}
		});

	});
});