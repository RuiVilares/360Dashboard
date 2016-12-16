$(document).ready(function(){
  var ref = getUrlParameter("treasury");
  
    ajaxConfig(); 


  $.ajax({url: "http://localhost:49822/api/treasury/get_apagar/" + ref, dataType: 'json', success: function(result){

    var total_aPagar = 0.0;
    for(var i = 0; i < result.length && i < 10; i++){
      if (result[i].m_Item2 != 0){
        total_aPagar += result[i].m_Item2;
        $(".aPagar").append('<tr><td>' + result[i].m_Item1 + '</td>' +
        "<td class='text-right'>" + parseFloat(result[i].m_Item2).toLocaleString() + '</td></tr>');
      }
    }

    $(".apagar").html(parseFloat(total_aPagar.toFixed(2)).toLocaleString());

  }});

  $.ajax({url: "http://localhost:49822/api/treasury/get_areceber/" + ref, dataType: 'json', success: function(result){

    var total_aReceber = 0.0;
    for(var i = 0; i < result.length && i < 10; i++){
      if (result[i].m_Item2 != 0){
        total_aReceber += result[i].m_Item2;
        $(".aReceber").append('<tr><td>' + result[i].m_Item1 + '</td>' +
        "<td class='text-right'>" + parseFloat(result[i].m_Item2).toLocaleString() + '</td></tr>');
      }
    }

    $(".areceber").html(parseFloat(total_aReceber.toFixed(2)).toLocaleString());

  }});
});
