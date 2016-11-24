$(function() {
  $.ajax({
    url: "http://localhost:49822/api/product/list",
    success: function (data) {
      for (var i = 0; i < data.length; i++){
        $('#navbarProducts').append("<li><a href='./product.html?product="+data[i].name+"'>"  + data[i].reference + "</a></li>");
      }
    },
    dataType: "json"
  });

  $.ajax({
    url: "http://localhost:49822/api/Clientes",
    success: function (data) {
      for (var i = 0; i < data.length; i++){
        $('#navbarClients').append("<li><a href='./client.html'>" /*?id=" + data[i].CodCliente +"'>"*/ + data[i].NomeCliente + "</a></li>");
      }
    },
    dataType: "json"
  });
  /*
  $.ajax({
    url: "http://localhost:49822/api/Fornecedores",
    success: function (data) {
      for (var i = 0; i < data.length; i++){
        $('#navbarProviders').append("<li><a href='provider.html'>" + data[i].NomeFornecedor + "</a></li>");
        console.log(data[i]);
      }
    },
    dataType: "json"
  });*/
});