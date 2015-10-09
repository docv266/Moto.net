
function enableField() {
    
    if ($('#PresenceMotoPerso').is(":checked"))
    {
        $("#conteneurMotoPerso").removeClass("hidden");
        $("#conteneurMotoProposee").addClass("hidden");
    }
    else
    {
        $("#conteneurMotoPerso").addClass("hidden");
        $("#conteneurMotoProposee").removeClass("hidden");
    }

}


$("#MotoPerso").tooltip({ placement: 'right' });
$("#FiltreMaMoto").tooltip({ placement: 'right' });
$("#FiltreMaMoto").attr("placeholder", "Choisir dans la liste : 3 caractères minimum");

