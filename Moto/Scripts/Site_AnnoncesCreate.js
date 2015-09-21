
function enableField(checkbox) {
    
    $("#idMotoPerso").prop("disabled", !checkbox.checked);
    $("select[name=MotoProposeeID").attr("disabled", checkbox.checked).trigger('chosen:updated');

}


$(".chosen-select").chosen({ placeholder_text_multiple: "Choisir plusieurs éléments", placeholder_text_single: "Choisir un élément", no_results_text: "Pas de résultat pour" });

$('.chosen-container').tooltip({ placement: 'right' });
$("#idMotoPerso").tooltip();

