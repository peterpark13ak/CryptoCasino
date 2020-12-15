function intSlotGameApp(images, board, totalRows, totalCols, callback) {
    let fullBoardNumbers = [];

    board.map(function (inArrays) {

        let objArr = [];

        inArrays.map(function (objects) {

            objArr.push(objects);

        });

        let final = [];
        objArr.map(function (elements) {

            let stringValue = Object.values(elements)[0];

            if (stringValue === "low") {
                final.push(1);
            }
            else if (stringValue === "medium") {
                final.push(2);
            }
            else if (stringValue === "high") {
                final.push(3);
            }
            else{
                final.push(4);
            }

        });

        fullBoardNumbers.push(final);
    });


    let slots = [];

    for (var i = 0; i <= totalRows; i += 1) {
        $('#slot_div').append(`<div class="row" id="ezslots${i}"></div>`);
    };

    for (var i = 0; i < totalRows; i += 1) {

        let rowElements = fullBoardNumbers[i];

        //TODO: FOR DEVELOPMENT TEST --> REMOVE ME!!
       // console.log(rowElements);
        if (i == 0) {
            slots.push(new EZSlots(`ezslots${i}`,
                {
                    "reelCount": totalCols,
                    "winningSet": rowElements,
                    "symbols": images,
                    "height": 80,
                    "width": 80,
                    "callback": callback
                }))
        }
        else {
            slots.push(new EZSlots(`ezslots${i}`,
                {
                    "reelCount": totalCols,
                    "winningSet": rowElements,
                    "symbols": images,
                    "height": 80,
                    "width": 80,
                }))
        }
        slots[i].win();
    };



};
