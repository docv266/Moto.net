
function enableField() {
    
    if ($('#Annonce_PresenceMotoPerso').is(":checked"))
    {
        $(".conteneurMotoPerso").removeClass("hidden");
        $(".conteneurMotoProposee").addClass("hidden");
    }
    else
    {
        $(".conteneurMotoPerso").addClass("hidden");
        $(".conteneurMotoProposee").removeClass("hidden");
    }

}

$("#Annonce_MotoPerso").tooltip({ placement: 'right' });

