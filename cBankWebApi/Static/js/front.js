
$( document ).ready(function() {
    console.log('start');
    $('.serve-button').click(function () {
        console.log('potwierdzenie');
        $(this).parent().parent().remove();
    });


    function serving_coffee() {
        $('tr').on('click', '.serve-button', function () {
            $(this).parent().parent().fadeOut();
        });
    }
    serving_coffee();

    var coffee_data = [
        {'price': '&#8364;6.00,-', 'coffee_name': 'double espresso', 'image': 'img/icon_espresso.png' },
        {'price': '&#8364;6.00,-', 'coffee_name': 'cappucino', 'image': 'img/icon_cappucino.png'},
        {'price': '&#8364;6.00,-', 'coffee_name': 'latte', 'image': 'img/icon_latte.png'}
    ];
    var semaphore = 1;
    function add_coffee(coffee) {
        if(semaphore)
            clearTimeout(semaphore);
        semaphore = setTimeout(function() {
                $('#coffee-list')
                    .prepend('<tr style="font-size: x-large">' +
                        '<td><img src="' +
                        coffee.image +
                        '" width="100"/></td>' +
                        '<td>' +
                        coffee.coffee_name +
                        '</td>' +
                        '<td>' +
                        coffee.price +
                        '</td>' +
                        '<td>Wojtek</td>' +
                        '<td>' +
                        '<button class="serve-button btn waves-effect waves-light" type="submit" name="action">Serve' +
                        '<i class="material-icons right">thumb_up</i>' +
                        '</button>' +
                        '</td>' +
                        '</tr>');
                $('#coffee-list > tr').first().hide().show(3000);
                serving_coffee();
            },
            1000);
    }
    
    $('#add-data').click(function () {
        add_coffee(coffee_data[1]);
    });

    if ($.connection != undefined) {
        var contosoChatHubProxy = $.connection.merchantNotificationHub;
        contosoChatHubProxy.client.addContosoChatMessageToPage = function(name, message) {
            console.log("new message" + message);
            add_coffee(coffee_data[message]);
        };
        $.connection.hub.start()
            .done(function() {
                console.log("hub started");
            });
    } else {
        console.log("mocking stuff");
    }

});
