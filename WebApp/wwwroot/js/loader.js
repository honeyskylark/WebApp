var images = document.images;
var images_total_count = images.length;
var images_loaded_count = 0;
var preloader = document.getElementById('page-preloader');

if (images_total_count != 0) {
    for (var i = 0; i < images_total_count; i++) {
        var image_clone = new Image();
        image_clone.src = images[i].src;
        image_clone.onload = image_loaded;
        image_clone.onerrror = image_loaded;
    }
    function image_loaded() {
        images_loaded_count++;
        if (images_loaded_count >= images_total_count) {
            setTimeout(function () {
                if (!preloader.classList.contains('done')) {
                    preloader.classList.add('done');
                }
            }, 100);
        }
    }
}
else {
    if (!preloader.classList.contains('done')) {
        preloader.classList.add('done');
    }
}

