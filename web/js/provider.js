$(document).ready(function(){
    ajaxConfig();

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

  setTimeout(function(){
    evolution($("#evolutionPivot").val());
  }, 50);


  $("#evolutionPivot").on("keyup paste mouseup", function(){
    $("#morris-area-chart").html("");
    evolution($("#evolutionPivot").val());
  });

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

function evolution(year){
  $.ajax({url: "http://localhost:49822/api/supplier/freq/" + getUrlParameter("provider").replace('.', '_') , dataType: 'json',data : {
            company: $.cookie("cmpny"),
            username : $.cookie("user"),
            password : $.cookie("pass"),
            year: year
        }, success: function(result){
    $('.evolution').html("Evolução");
    Morris.Line({
      element : "morris-area-chart",
      data : result,
      pointSize: 10,
      xkey : "date",
     postUnits: "€",
      ykeys : ["value", "valuePrev"],
      labels : ["Ano Atual ("+year+")", "Ano Anterior("+(year-1)+")"],
      resize: true
    });
  }});
}
