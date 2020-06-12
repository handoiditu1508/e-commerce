$(document).ready(function () {
	//upload images
	$('.registerProductFileInput').fileinput({
		allowedFileExtensions: ['jpg', 'png', 'gif', 'jpeg'],
		removeClass: 'btn btn-danger',
		showUpload: false,
		allowedFileTypes: ['image'],
		maxFileSize: '5120',//5Mb
		theme: 'fas',
		browseOnZoneClick: 'true'
	});
});