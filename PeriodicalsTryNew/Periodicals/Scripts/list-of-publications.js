$(document).ready(function () {
    $('.list').click(function (event) { event.preventDefault(); $('#products .item').removeClass('col-xs-4 col-lg-4'); $('#products .item').addClass('list-group-item'); });
    $(".grid").click(function (event) { event.preventDefault(); $('#products .item').removeClass('list-group-item'); $('#products .item').addClass('col-xs-4 col-lg-4'); $('#products .item').addClass('grid-group-item'); });
});
