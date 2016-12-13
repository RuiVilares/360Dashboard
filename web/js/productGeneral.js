$(document).ready(function(){
  var ref = getUrlParameter("product");

  $.ajax({url: "http://localhost:49822/api/product/Get_top10p/" + ref, dataType: 'json', success: function(result){
    for(var i = 0; result.length && i < 10; i++){
      result[i].valor = result[i].valor.toFixed(2);
    }
    Morris.Bar({
        "element" : "morris-bar-chart",
        "data" : result.slice(0, 10),
        "xkey" : "name",
        "ykeys": ["valor"],
        "labels": ["Vendas"],
        "resize" : true
    });

    for(var i = 0; i < result.length && i < 10; i++){
        $(".shipments").prepend(
            "<tr>" +
                "<td>" + result[i].name + "</td>" +
                "<td><a href='product.html?product="+result[i].reference+"'>" + result[i].reference + "</a></td>" +
                "<td>" + parseFloat(result[i].valor).toLocaleString() + "</td>" +
            "</tr>"
        );
    }


    $(".melhores_c").html("Melhores Produtos");

  }});

  $.ajax({url: "http://localhost:49822/api/product/Get_Evolution/" + ref, dataType: 'json', success: function(result){
    console.log(result);

    for(var k in result){
        result[k].value = result[k].value.toFixed(2);
        result[k].valuePrev = result[k].valuePrev.toFixed(2);
    }

    Morris.Area({
      "element" : "morris-area-chart",
      "data" : result,
      pointSize: 10,
      "xkey": 'date',
      "ykeys": ['value', 'valuePrev'],
      "yLabelFormat": function(y){return y.toFixed(2)},
      "postUnits": "€",
      "labels": ['Ano Atual', 'Ano Transacto'],
      "resize" : true
    });


  }});

  $.ajax({url: "http://localhost:49822/api/product/products_info/" + ref, dataType: 'json', success: function(result){
     $(".turnover").html(parseFloat(result[0].m_Item2.toFixed(2)).toLocaleString());
     $(".value").html(parseFloat(result[1].m_Item2.toFixed(2)).toLocaleString());

   }});



});
