$(document).ready(function () {
    $.getScript('http://cdnjs.cloudflare.com/ajax/libs/select2/3.4.8/select2.min.js', function () {

        /* dropdown and filter select */
        $('.select2').select2({

        });

        $('select.select2-no-clear').select2({

        });
    }); //script
});