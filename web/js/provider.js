$(document).ready(function(){
  var id = getUrlParameter("provider");

  $.ajax({url: "http://localhost:49822/api/supplier/detail/" + id.replace('.', '_') , dataType: 'json', success: function(result){
    $(".providerName").html(result.name);
    $(".providerAddress").html(result.address);
    $(".providerPostCode").html(result.post_c);
    $(".providerLocalidade").html(result.city);
    $(".providerRef").html(result.reference);
    $(".pendente").text(parseFloat(result.pendente.toFixed(2)).toLocaleString());
    $(".backlog").text(parseFloat(result.backlog.toFixed(2)).toLocaleString());
  }});

  $.ajax({url: "http://localhost:49822/api/supplier/freq/" + id.replace('.', '_') , dataType: 'json', success: function(result){
    $('.evolution').html("Evolução");
    Morris.Line({
      element : "morris-area-chart",
      data : result,
      pointSize: 0,
      xkey : "m_Item1",
      ykeys : ["m_Item2"],
      labels : ["Volume de Compras"],
      resize: true
    });
  }});

  $.ajax({url: "http://localhost:49822/api/supplier/topprod/" + id.replace('.', '_') , dataType: 'json', success: function(result){
    $('.topprod').html("Mais Comprados");

    result = result.sort(function compare(a,b){
      if (a.order_p < b.order_p)
      return 1;
      else
      return -1;
    }).slice(0, 10);

    var chart = [];

    for(var i = 0; i < result.length; i++){
      chart.push({label : "Produto: " + result[i].name + " (%)", value : parseFloat(result[i].order_p.toFixed(2)).toLocaleString()});
    }

    Morris.Donut({
      element : "morris-pie",
      data : chart ,
      resize: true
    });
  }});
});
