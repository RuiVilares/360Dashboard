$(function() {
  $.ajax({
    url: "http://localhost:49822/api/artigos/" + $("#page-wrapper").data("id"),
    success: function (data) {
      $(".row").removeClass("hidden");
      $('.productName').text(data.DescArtigo);
    },
    dataType: "json"
  });
});
