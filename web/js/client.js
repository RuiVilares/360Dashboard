$(function() {
  $.ajax({
    url: "http://localhost:49822/api/clientes/" + $("#page-wrapper").data("id"),
    success: function (data) {
      var morada = data.Morada.split(',');
      $('.clientName').text(data.NomeCliente);
      $('#clientStreet').text(morada[0]);
      $('#clientZip').text(morada[1]);
      $('#clientNIF').text(data.NumContribuinte);
      $(".row").removeClass("hidden");
    },
    dataType: "json"
  });
});
