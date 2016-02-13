var app = angular.module("MakeDepositApp", []);

app.controller("ConversionController", ['$scope', function ($scope) {
    $scope.setText = function (conversionRate) {

        var value = $scope.radio;
        var amount = $scope.filledInAmount;
        if (value === "€") {
            $scope.convertedAmount = "B " + convertEuroToBitcoins(amount, conversionRate) + " will be transfered to your Crypto Account.";
            document.getElementById("amount").value = convertEuroToBitcoins(amount, conversionRate);
        }
        if (value === "B") {
            $scope.convertedAmount = "B " + amount + " will be transfered to your Crypto Account. This is equivalent to € " + convertBitcoinsToEuro(amount, conversionRate) + ".";
            document.getElementById("amount").value = amount;
        }
    }
}])

function convertBitcoinsToEuro(amount, conversionRate) {
    return amount * conversionRate;
}

function convertEuroToBitcoins(amount, conversionRate) {
    return amount * (1 / conversionRate);
}