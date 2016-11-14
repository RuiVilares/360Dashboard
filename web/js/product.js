$(function() {
  $.ajax({
    url: "http://localhost:49822/api/artigos/A0001",
    success: function (data) {
      $('.productName').text(data.DescArtigo);
      console.log(data.DescArtigo);
    },
    dataType: "json"
  });
});
