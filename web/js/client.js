$(document).ready(function(){
  var id = getUrlParameter("client");

  $.ajax({url: "http://localhost:49822/api/Clientes/detail/" + id.replace(/ /g, "_").replace(/\./g, '_') , dataType: 'json', success: function(result){
      $(".clientName").html(result.name);
      $(".clientID").html(result.id);
      $(".clientLocalidade").html(result.city);
      $(".postCode").html(result.post_c);
      $(".clientAddress").html(result.address);
  }});

  $.ajax({url: "http://localhost:49822/api/Clientes/range/" + id.replace(/ /g, "_").replace(/\./g, '_') , dataType: 'json', success: function(result){
      $('.evolution').html("Evolução");

      var month = result.map(function(el){
        return {month: moment(el.date).month(), year: moment(el.date).year(), value: el.value};
      });

      Morris.Line({
          element : "morris-area-chart",
          data : result,
          pointSize: 0,
          xLabels: "month",
          xkey : "date",
          ykeys : ["value"],
          labels : ["Volume de Compras"],
          resize: true
      });
  }});

  $.ajax({url: "http://localhost:49822/api/Clientes/topprod/" + id.replace(/ /g, "_").replace(/\./g, '_') , dataType: 'json', success: function(result){
      $('.topprod').html("Mais Comprados");

      var chart = [];

      for(var i = 0; i < result.length; i++){
          chart.push({label : "Produto: " + result[i].reference + " (%)", value : result[i].sales_p.toFixed(2)});
      }

      Morris.Donut({
          element : "morris-pie",
          data : chart ,
          resize: true
      });
  }});
});
