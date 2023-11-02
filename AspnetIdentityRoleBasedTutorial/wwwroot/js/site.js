// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener('DOMContentLoaded', function () {
    const slide = document.getElementById('slide');
    const items = document.querySelectorAll('.item');
    const captions = document.querySelectorAll('.caption');
    const prevButton = document.getElementById('prev');
    const nextButton = document.getElementById('next');
    let currentIndex = 0;

    function showItem(index) {
        items.forEach((item, i) => {
            if (i === index) {
                item.style.display = 'block';
            } else {
                item.style.display = 'none';
            }
        });
    }

    function showCaption(index) {
        captions.forEach((caption, i) => {
            if (i === index) {
                // Set your caption text here based on the image index
                caption.textContent = "Caption for Image " + (index + 1);
            } else {
                caption.textContent = ""; // Clear the caption for hidden images
            }
        });
    }

    function showNextItem() {
        currentIndex = (currentIndex + 1) % items.length;
        showItem(currentIndex);
        showCaption(currentIndex);
    }

    function showPrevItem() {
        currentIndex = (currentIndex - 1 + items.length) % items.length;
        showItem(currentIndex);
        showCaption(currentIndex);
    }

    nextButton.addEventListener('click', showNextItem);
    prevButton.addEventListener('click', showPrevItem);

    // Initial display
    showItem(currentIndex);
    showCaption(currentIndex);
});





