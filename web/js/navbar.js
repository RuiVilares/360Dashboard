$(document).ready(function() {

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
    url: "http://localhost:49822/api/Clientes/list",
    success: function (data) {
      for (var i = 0; i < data.length; i++){
        $('#navbarClients').append("<li><a href='./client.html?client="+data[i].id+"'>" /*?id=" + data[i].CodCliente +"'>"*/ + data[i].name + "</a></li>");
      }
    },
    dataType: "json"
  });

  $.ajax({
    url: "http://localhost:49822/api/supplier/list",
    success: function (data) {
      for (var i = 0; i < data.length; i++){
        $('#navbarProviders').append("<li><a href='provider.html?provider="+data[i].reference+"'>" + data[i].name + "</a></li>");

      }
    },
    dataType: "json"
  });
});

$(document).ajaxSuccess(function(event, xhr, settings){
  if(settings.url.match("http://localhost:49822/api/product/list.*")){
    $("#prodSearch").on("change keyup paste mouseup", function(){

      var result = JSON.parse(xhr.responseText).filter(function(el){
        return el.reference.match($("#prodSearch").val() + ".*") || el.name.match($("#prodSearch").val() + ".*");
      });

      $("#navbarProducts").html("");
      for (var i = 0; i < result.length; i++){
          $('#navbarProducts').append("<li><a href='./product.html?product="+result[i].name+"'>"  + result[i].reference + "</a></li>");
      }
    });
  }
  if(settings.url.match("http://localhost:49822/api/Clientes/list.*")){
    $("#clientSearch").on("change keyup paste mouseup", function(){

      var result = JSON.parse(xhr.responseText).filter(function(el){
        return el.name.match($("#clientSearch").val() + ".*") || el.id.match($("#clientSearch").val() + ".*");
      });

      $("#navbarClients").html("");
      for (var i = 0; i < result.length; i++){
          $('#navbarClients').append("<li><a href='./client.html?client="+result[i].id+"'>" /*?id=" + data[i].CodCliente +"'>"*/ + result[i].name + "</a></li>");
      }
    });
  }
  if(settings.url.match("http://localhost:49822/api/supplier/list.*")){
    $("#suppSearch").on("change keyup paste mouseup", function(){

      var result = JSON.parse(xhr.responseText).filter(function(el){
        return el.name.match($("#suppSearch").val() + ".*") || el.reference.match($("#suppSearch").val() + ".*");
      });

      $("#navbarProviders").html("");
      for (var i = 0; i < result.length; i++){
          $('#navbarProviders').append("<li><a href='provider.html?provider="+result[i].reference+"'>" + result[i].name + "</a></li>");
      }
    });
  }
});
