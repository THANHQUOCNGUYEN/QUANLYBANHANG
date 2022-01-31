function ShowImagePreview(imageUploader, abc) {
    if (imageUploader.files && imageUploader.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(abc).attr('src', e.target.result);

        }
        reader.readAsDataURL(imageUploader.files[0]);
    }
}