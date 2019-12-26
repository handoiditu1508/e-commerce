$(document).ready(function () {

	$('.addToCartOnSubmit').submit(function(event) {
		// Stop form from submitting normally
		event.preventDefault();
		let $form = $(this);
		$.ajax({
			url: $form.attr('action'),
			type: $form.attr('method'),
			data: $form.serialize(),
			success: function () {
				updateSmallCart();
			},
			error: function (result) {
				showErrors(['Something went wrong while adding to cart', JSON.stringify(result)]);
			}
		});
	});

	//product detail pics
	$('.galleryThumbnail').click(function() {
		$('#galleryResult').html($(this).clone());
	});

	//control product attributes states
	/*let attributeKeys = ["gender", "color", "size"];*/
	let attributeKeys;

	/*
	let attributes = {
		gender: ["male", "female", "other"],
		color: ["red", "blue", "green"],
		size: ["s", "m", "l"]
	};*/
	let attributes;

	/*
	let attributesStates = [
		{gender: "male", color: "green", size: "l"},
		{gender: "male", color: "green", size: "s"},
		{gender: "male", color: "blue", size: "l"},
		{gender: "male", color: "blue", size: "m"},
		{gender: "male", color: "red", size: "m"},
		{gender: "male", color: "red", size: "s"},
		{gender: "female", color: "red", size: "s"},
		{gender: "female", color: "red", size: "l"},
		{gender: "female", color: "red", size: "m"},
		{gender: "female", color: "green", size: "s"},
		{gender: "female", color: "green", size: "l"},
		{gender: "female", color: "green", size: "m"},
		{gender: "other", color: "blue", size: "m"},
		{gender: "other", color: "blue", size: "l"},
	];*/
	let attributesStates;

	//get selector for jquery that select attributes by name
	function getSelectorString(name) {
		return "input[name^='attributes[" + name + "']";
	}

	//get selector for jquery that select attributes
	function getSelectorStringAll() {
		return "input[name^='attributes[']";
	}

	//initiate attributes container variables
	function loadVariables() {
		attributeKeys = [];
		attributes = {};
		attributesStates = [];

		let sellerId = $('#sellerId').val();
		let productTypeId = $('#productTypeId').val();

		let executeCheckFirstNext = false;
		//initiate attributes from server
		$.ajax({
			url: $('#getProductAttributesUrl').val(),
			type: $('#getProductAttributesUrl').attr("urlMethod"),
			data: {
				sellerId: sellerId,
				productTypeId: productTypeId
			},
			success: function (result) {
				if (executeCheckFirstNext) {
					attributes = result;
					//initiate attributeKeys from attributes's properties name
					for (prop in attributes) {
						if (Object.prototype.hasOwnProperty.call(attributes, prop)) {
							attributeKeys.push(prop);
						}
					}

					checkFirst();
				}
				else {
					executeCheckFirstNext = true;

					attributes = result;
					//initiate attributeKeys from attributes's properties name
					for (prop in attributes) {
						if (Object.prototype.hasOwnProperty.call(attributes, prop)) {
							attributeKeys.push(prop);
						}
					}
				}
			},
			error: function (result) {
				showErrors(['Something went wrong while loading attributes', JSON.stringify(result)]);
			}
		});

		//initiate attributesStates from server
		$.ajax({
			url: $('#getProductAttributesStatesUrl').val(),
			type: $('#getProductAttributesStatesUrl').attr("urlMethod"),
			data: {
				sellerId: sellerId,
				productTypeId: productTypeId
			},
			success: function (result) {
				if (executeCheckFirstNext) {
					attributesStates = result;
					checkFirst();
				}
				else {
					executeCheckFirstNext = true;
					attributesStates = result;
				}
			},
			error: function (result) {
				showErrors(['Something went wrong while loading attributes states', JSON.stringify(result)]);
			}
		});
	}

	//get index of name in attributeKeys
	function getAttributeKeysIndexByName(name) {
		for (i = 0; i < attributeKeys.length; i++) {
			if (attributeKeys[i] == name) {
				return i;
			}
		}
		return -1;
	}

	//after check the an attribute call this function to reconfig other attributes
	//keyName is the Name of the attribute. ex: gender, color, size
	function configAttributesAfterChecked(keyName) {
		disableFromIndex(getAttributeKeysIndexByName(keyName) + 1);

		let shrinkingArray = attributesStates;
		let found = false;
		for (i = 0; i < attributeKeys.length; i++) {
			let prop = attributeKeys[i];

			if (keyName == prop) {
				found = true;
			}
			else if (found) {
				unCheckName(prop);
				for (j = 0; j < shrinkingArray.length; j++) {
					let slctor = $(getSelectorString(prop) + "[value='" + shrinkingArray[j][prop] + "']");
					slctor.prop('checked', true);
					slctor.prop('disabled', false);
				}
			}

			let chcked = getChecked(prop);
			shrinkingArray = shrinkingArray.filter(attr => attr[prop] == chcked);
		}
	}

	//check the valid default attributes state
	function checkFirst() {
		if (attributeKeys == null || attributeKeys.length < 1 && attributesStates == null && attributesStates.length < 1)
			return;

		let prop = attributeKeys[0];
		disableName(prop);
		for (i = 0; i < attributesStates.length; i++) {
			let slctor = $(getSelectorString(prop) + "[value='" + attributesStates[i][prop] + "']");
			slctor.prop('checked', true);
			slctor.prop('disabled', false);
		}
		$(getSelectorString(attributeKeys[0]) + "[value='" + attributesStates[0][attributeKeys[0]] + "']").prop('checked', true);

		configAttributesAfterChecked(attributeKeys[0]);
	}

	//uncheck attribute by name. ex: gender, color, size
	function unCheckName(name) {
		$(getSelectorString(name)).prop('checked', false);
	}

	//uncheck all attributes
	function unCheckAll() {
		$(getSelectorStringAll()).prop('checked', false);
	}

	//disable all attributes
	function disableAll() {
		$(getSelectorStringAll()).prop('disabled', true);
	}

	//disable attributes by name
	function disableName(name) {
		$(getSelectorString(name)).prop('disabled', true);
	}

	//disable attributes from index base on attributeKeys index
	function disableFromIndex(index) {
		for (i = index; i < attributeKeys.length; i++) {
			$(getSelectorString(attributeKeys[i])).prop('disabled', true);
		}
	}

	//enable attribute by name. ex: gender, color, size
	function enableName(name) {
		$(getSelectorString(name)).prop('disabled', false);
	}

	//input = "attributes[gender]"
	//output = "gender"
	function getKeyName(str) {
		return str.substring(str.indexOf("[") + 1, str.length - 1);
	}

	//get value from the checked attribute by name
	//ex: name = "color". Value can be on of: red, green, blue
	function getChecked(name) {
		return $(getSelectorString(name) + ':checked').val();
	}

	//execute when attribute is checked
	$(getSelectorStringAll()).change(function (event) {
		if (!this.checked)
			return;

		configAttributesAfterChecked(getKeyName(this.name));
	});

	//perform functions
	loadVariables();
});