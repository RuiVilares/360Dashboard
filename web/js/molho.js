$(document).ready(function(){
  $("#molhoButton").click(function(){
    $("#panel").append("<img src='../data/molho.png' style='position: absolute; top: " + getRandom()  +"px; left: " + getRandom() + "px" +   "'>");
  });
});


function getRandom(){
  return Math.floor(Math.random() * 1000);
}
