
$(document).ready(function () {
    const url = "https://bitpay.com/api/rates"
    let filteredData = [];
    $('#webticker-3').html('');


    $.ajax({
        url: url,
        type: 'GET',
        async: true,
        dataType: 'json',
        success: function (data) {
            filteredData = data.filter(item => {
                return item.code == "BTC" || item.code == "BCH" || item.code == "ETH" || item.code == "LTC" || item.code == "XRP" || item.code == "BUSD" || item.code == "DAI" || item.code == "DOGE" || item.code == "USDC" || item.code == "SHIB";
            });
            $.each(filteredData, function (i, item) {
                $('#webticker-3').append(`<li class="br-1">
                                <div class="mx-20">
                                    <div class="d-flex justify-content-center">
                                        <h6 class="font-weight-300 mr-5">${item.code}</h6>
                                    </div>
                                    <div class="d-block text-center">
                                        <h3 class=" font-weight-400 my-0">${(data[2].rate / item.rate).toFixed(2)}<span>USD</span></h3>
                                        <p class="mb-0"><span class="font-weight-300">Volum</span><span class="px-5">${item.rate}</span><span>BTC</span></p>
                                    </div>
                                </div>
                            </li>`)
            });
            
        },
        error: function (request, error) {
            console.log("Request: " + JSON.stringify(request));
        }
    });
});