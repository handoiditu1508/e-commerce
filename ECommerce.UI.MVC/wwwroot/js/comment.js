$(document).ready(function () {
	$('.comment-star').rating({
		min: 0,//data-min
		max: 5,//data-max
		step: 1,//data-step
		displayOnly: true,//data-display-only
		showClear: false,//data-show-clear
		showCaption: false,//data-show-caption
		size: 'xs',//data-size
		containerClass: 'float-right',//data-container-class
		filledStar: '<i class="fa fa-star text-warning"></i>',
		emptyStar: '<i class="fa fa-star text-secondary"></i>',
		//clearButton: '<a><i class="fa fa-minus-circle"></i><a/>'
	});

	$('.h-equal-w').each(function (index, element) {
		$(this).css("height", $(this).css("width"));
	});
});