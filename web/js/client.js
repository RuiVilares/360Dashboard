$(document).ready(function(){
  ajaxConfig();

    var id = getUrlParameter("client");

  $.ajax({url: "http://localhost:49822/api/Clientes/detail/" + id.replace(/ /g, "_").replace(/\./g, '_') , dataType: 'json', success: function(result){
      $(".clientName").html(result.name);
      $(".clientID").html(result.id);
      $(".clientLocalidade").html(result.city);
      $(".postCode").html(result.post_c);
      $(".clientAddress").html(result.address);
      $(".pendentes").html(parseFloat(result.pendentes.toFixed(2)).toLocaleString());
      $(".divida").html(parseFloat(result.divida.toFixed(2)).toLocaleString());
  }});

  setTimeout(function(){
    evolution($("#evolutionPivot").val());
  }, 50);

  $("#evolutionPivot").on("keyup paste mouseup", function(){
    $("#morris-area-chart").html("");
    evolution($("#evolutionPivot").val());
  });

  $.ajax({url: "http://localhost:49822/api/Clientes/topprod/" + id.replace(/ /g, "_").replace(/\./g, '_') , dataType: 'json', success: function(result){
      $('.topprod').html("Mais Comprados");

      var chart = [];

      for(var i = 0; i < result.length; i++){
          chart.push({label : "Produto: " + result[i].reference + " (%)", value : parseFloat(result[i].sales_p.toFixed(2)).toLocaleString()});
      }

      Morris.Donut({
          element : "morris-pie",
          data : chart ,
          resize: true
      });
  }});
});


function evolution(year){
  $.ajax({url: "http://localhost:49822/api/Clientes/range/" + getUrlParameter("client").replace(/ /g, "_").replace(/\./g, '_') , dataType: 'json',data : {
            company: $.cookie("cmpny"),
            username : $.cookie("user"),
            password : $.cookie("pass"),
            year: year
        },
     success: function(result){
      $('.evolution').html("Evolução");

      var month = result.map(function(el){
        return {month: moment(el.date).month(), year: moment(el.date).year(), value: el.value};
      });

      Morris.Line({
          element : "morris-area-chart",
          data : result,
          pointSize: 10,
          xLabels: "month",
          xkey : "date",
          ykeys : ["value", "valuePrev"],
          xmin: 12,
          labels : ["Ano Corrente (2016)", "Ano Transacto (2015)"],
          postUnits: "€",
          parseTime: false,
          resize: true
      });
  }});

}
