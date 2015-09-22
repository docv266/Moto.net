
function enableField() {
    
    $("#conteneurMotoPerso").toggleClass("hidden");

    $("#conteneurMotoProposee").toggleClass("hidden");

}


$(".chosen-select").chosen({ placeholder_text_multiple: "Choisir plusieurs éléments", placeholder_text_single: "Choisir un élément", no_results_text: "Pas de résultat pour" });

$('.chosen-container').tooltip({ placement: 'right' });
$("#MotoPerso").tooltip({ placement: 'right' });


$(document).ready(function () {

    var c = document.getElementById('PresenceMotoPerso');

    if (c.checked)
    {
        enableField();
    }

});