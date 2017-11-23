/// <reference path="../jquery-3.1.1.js" />

//Sample ways to get a image and load to image tag
function ForTest() {
    $.ajax({
        url: '/svg_icons/Sample.svg',
        success: function (data) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $("#q").attr("src", e.target.result);
            }
            reader.readAsDataURL(new Blob([data.documentElement.outerHTML], { type: "image/svg+xml" }));
        },
        error: function (e) {
            console.log(e);
        }
    })

    fetch('/images/icon.png').then(function (data) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $("#q").attr("src", e.target.result);
        }
        data.blob().then(function (b) { reader.readAsDataURL(b); });
    })
}