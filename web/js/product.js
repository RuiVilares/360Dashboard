$(document).ready(function() {
    ajaxConfig(); 
    
    var ref = getUrlParameter("product");
    $.ajax({url: "http://localhost:49822/api/product/Get_Info/" + ref.replace(/ /g, "_").replace(/\./g, '_'), dataType: 'json', success: function(result){
        console.log(result);
        $(".productName").html(result.name);
        $(".profit_margin").html((result.profit_margin[0] * 100).toFixed(1) + "%");
        $(".pvp").html(parseFloat(result.retail[0].toFixed(2)).toLocaleString());
        $(".price").html(parseFloat(result.price.toFixed(2)).toLocaleString());
        $(".tax").html(parseFloat(result.tax).toFixed(2) + "%");
        $(".reference").html(result.reference);
        $(".value_stock").html(parseFloat(result.stkValue.toFixed(2)).toLocaleString());
        $(".qnt_stock").html(result.stk.toLocaleString() +" " + result.unit);

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
                    "<td>" + result[i].quantity.toLocaleString() + "</td>" +
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
    $("#PVP1").click(function(){changePVP(result.retail[0].toLocaleString(), (result.profit_margin[0] * 100).toFixed(1) + "%")});
    $("#PVP2").click(function(){changePVP(result.retail[1].toLocaleString(), (result.profit_margin[1] * 100).toFixed(1) + "%")});
    $("#PVP3").click(function(){changePVP(result.retail[2].toLocaleString(), (result.profit_margin[2] * 100).toFixed(1) + "%")});
    $("#PVP4").click(function(){changePVP(result.retail[3].toLocaleString(), (result.profit_margin[3] * 100).toFixed(1) + "%")});
    $("#PVP5").click(function(){changePVP(result.retail[4].toLocaleString(), (result.profit_margin[4] * 100).toFixed(1) + "%")});
    $("#PVP6").click(function(){changePVP(result.retail[5].toLocaleString(), (result.profit_margin[5] * 100).toFixed(1) + "%")});



  }

});

function changePVP(val1, val2){
  $(".pvp").html(val1);
  $(".profit_margin").html(val2);
}
