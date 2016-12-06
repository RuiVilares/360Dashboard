$(document).ready(function() {
    var ref = getUrlParameter("product");
    $.ajax({url: "http://localhost:49822/api/product/Get_Info/" + ref.replace(/ /g, "_").replace(/\./g, '_'), dataType: 'json', success: function(result){
      //console.log(result);
        $(".productName").html(result.name);
        $(".profit_margin").html((result.profit_margin[0] * 100).toFixed(1) + "%");
        $(".pvp").html(result.retail[0]);
        $(".price").html(result.price);
        $(".tax").html(result.tax + "%");
        $(".reference").html(result.reference);
    }});


    $.ajax({url: "http://localhost:49822/api/product/Get_Top10c/" + ref.replace(/ /g, "_").replace(/\./g, '_'), dataType: 'json', success: function(result){
        Morris.Bar({
            "element" : "morris-bar-chart",
            "data" : result,
            "xkey" : "name",
            "ykeys": ["valor"],
            "labels": ["Vendas"],
            "resize" : true
        });

        $(".melhores_c").html("Melhores Clientes");

    }});

    $.ajax({url: "http://localhost:49822/api/product/get_shipments/" + ref.replace(/ /g, "_").replace(/\./g, '_'), dataType: 'json', success: function(result){
        for(var i = 0; i < result.length && i < 10; i++){
            $(".shipments").prepend(
                "<tr>" +
                    "<td>" + result[i].client + "</td>" +
                    "<td>" + result[i].quantity + "</td>" +
                    "<td>" + result[i].shipmentDate + "</td>" +
                "</tr>"
            );
        }

        $(".expedicoes_r").html("Expedições Recentes");
    }});

});

$(document).ajaxSuccess(function(event, xhr, settings){
  if(settings.url.match("http://localhost:49822/api/product/Get_Info/.*")){
    var result = JSON.parse(xhr.responseText);
    console.log(result);
    $("#PVP1").click(function(){changePVP(result.retail[0], (result.profit_margin[0] * 100).toFixed(1) + "%")});
    $("#PVP2").click(function(){changePVP(result.retail[1], (result.profit_margin[1] * 100).toFixed(1) + "%")});
    $("#PVP3").click(function(){changePVP(result.retail[2], (result.profit_margin[2] * 100).toFixed(1) + "%")});
    $("#PVP4").click(function(){changePVP(result.retail[3], (result.profit_margin[3] * 100).toFixed(1) + "%")});
    $("#PVP5").click(function(){changePVP(result.retail[4], (result.profit_margin[4] * 100).toFixed(1) + "%")});
    $("#PVP6").click(function(){changePVP(result.retail[5], (result.profit_margin[5] * 100).toFixed(1) + "%")});



  }

});

function changePVP(val1, val2){
  $(".pvp").html(val1);
  $(".profit_margin").html(val2);
}
