$(document).ready(function () {
	$("[data-toggle=tooltip]").tooltip();

	//click tag a will trigger input with the same name
	$(".status-upload .trigger-input").click(function (event) {
		event.preventDefault();
		let input = $(this).closest(".status-upload").find("input[name='" + $(this).attr("name") + "']");
		input.trigger("click");
	});

	//preview images
	function readURL(input) {
		if (input.files) {
			for (let i = 0; i < input.files.length; i++) {
				let reader = new FileReader();

				reader.onload = function (e) {
					let container = document.createElement("div");
					container.classList.add("comment-image-preview");
					let img = document.createElement("img");
					img.src = e.target.result;

					container.appendChild(img);
					$(input).closest(".status-upload").find(".comment-images-preview").append(container);
				}

				reader.readAsDataURL(input.files[i]);
			}
		}
	}

	$(".status-upload input[class='upload-comment-images']").change(function () {
		$(this).closest(".status-upload").find(".comment-images-preview").empty();
		readURL(this);
	});

	$('.upload-comment-star').rating({
		min: 0,//data-min
		max: 5,//data-max
		step: 1,//data-step
		displayOnly: false,//data-display-only
		showClear: false,//data-show-clear
		showCaption: true,//data-show-caption
		size: 'xs',//data-size
		containerClass: '',//data-container-class
		filledStar: '<i class="fa fa-star text-warning"></i>',
		emptyStar: '<i class="fa fa-star text-secondary"></i>',
		//clearButton: '<a><i class="fa fa-minus-circle"></i><a/>'
	});
});