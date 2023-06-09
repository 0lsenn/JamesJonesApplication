﻿/**
* Instanciates event listeners upon page load
*/
window.addEventListener("load", async (e) => {

    console.log("This Happend: ")
    //refreshes the DDL
    updateDDL()

    //Carries submitted form into the controller
    document.getElementById("createListForm").addEventListener('submit', async (e) => {
        await handleCreateList(e);
    });

    //Spawns the Create List Modal
    document.getElementById("btnAddNewListModal").addEventListener('click', (e) => {
        console.log("Spawn Create Modal Fired");
        $('#createListModal').modal('show')
    });

    //Spawns the Create List Modal
    document.getElementById("btnDeleteList").addEventListener('click', async (e) => {
        console.log("Delete Attempt Fired");
        deleteShoppingListConfirm(e);

    });

    //Adds Event Listeners to Remove Item button
    ApplyRemoveItemListener();

});


async function ApplyRemoveItemListener() {
    //get reference from the item container
    let itemContainer = document.getElementById('tableBodyContainer');
    
    //find corresponding object
    let DeleteItemButton = itemContainer.querySelectorAll('button[value="Remove"]');
    let buttonArray = Array.from(DeleteItemButton);

    console.log("We reached This far");

    buttonArray.forEach((value, index) => {

        value.addEventListener('click', (e) => {
            console.log("Button Identified with value: " + value.dataset.itemid)
            //deleteShoppingListConfirm(value.dataset.itemid);
        })
    });

   
}

//Create a Shopping List modal - or call the modal basically
async function ShowCreateListModal() {
    //Call controller partial view
    let response = await advFetch('/ShoppingList/AddNewShoppingList');

    let htmlResponse = await response.text();
    //Set it to modal template
    document.getElementById('ModalBody').innerHTML = htmlResponse;
    document.getElementById('ModalTitle').innerHTML = 'Create New List';

    //form reference
    let formReference = document.querySelector('form[action="/SamsWareHouseItem/CreateNewShopItem"]');

    //check form on console
    console.log(formReference);

    //Button event listener when user submits, call another method
    formReference.addEventListener('submit', (e) => { handleCreateList(e) });

    //show the modal
    $('#createListModal').modal('show');

}//end spawn modal

/**
 * Updates The Drop Down List
 */
async function updateDDL() {
    //from controller
    let result = await fetch('/ShoppingList/ShoppingListDDL');
    let htmlResult = await result.text();

    //get container and set as the template for th partial view
    document.getElementById('selectContainer').innerHTML = htmlResult;

    //place it
    let ddlContainer = document.getElementById('selectContainer');

    //get item based on user event changed
    let ddl = ddlContainer.querySelector('select');
    ddl.addEventListener('change', async (e) => {
        handleDDLChange(e);
    });

}

/**
 * Handle changes made or on the DDL by identifying itself's slected option
 */
async function handleDDLChange(e) {
    let selectedOption = e.target.selectedOptions[0]
    //set values
    sessionStorage.setItem('listID', selectedOption.value)
    sessionStorage.setItem('listName', selectedOption.text)
    //send paramters to te controller
    let result = await fetch('/ShoppingList/GetSamsWareHouseItemForList?listID=' + selectedOption.value);
    let htmlResult = await result.text();
    //refresh
    document.getElementById('shopItemContainer').innerHTML = htmlResult
}


/**
 * Function that handles creation of a list
 */
async function handleCreateList(e) {
    e.preventDefault();

    //get list name
    console.log(e.target["listName"].value);

    //null check
    let nullOrWhiteSpaceChecked = e.target["listName"].value;
    let listName = nullOrWhiteSpaceChecked.trim();
    //null indicate
    if (listName.length <= 4 && listName.length < 32 || listName == "") {
        return alert("Please Dont Leave the List name empty.")
    }

    //call controller and send the JSON object
    let result = await fetch('/ShoppingList/AddNewShoppingList', {
        method: 'POST',
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify(e.target["listName"].value)
    });

    //if success
    if (result.ok) {
        //update dropdown list and hide the modal view
        await updateDDL();
        $('#createListModal').modal('hide');
    }

}

/**
 * Remove a single item from the list
 */
async function removeItemFromList(itemId) {
    let listID = sessionStorage.getItem("listID");

    //Assign values
    let shoppingListItem =
    {
        ShoppingListID: listID,
        SamsWareHouseItemId: itemId
    }

    //Call controller and send object to delete
    let result = await fetch('/ShoppingList/RemoveItemFromList', {
        method: 'DELETE',
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify(shoppingListItem)
    });

    //if success
    if (result.ok) {
        // Update Shopping List Table

        let result = await fetch('/ShoppingList/GetSamsWareHouseItemForList?listID=' + listID);
        let htmlResult = await result.text();
        document.getElementById('shopItemContainer').innerHTML = htmlResult
    }
}

/**
 * Deletes a shopping list function
 */
async function deleteShoppingListConfirm() {
    //get and set list Id
    let listID = sessionStorage.getItem("listID");
    //reassign
    let shoppingListItemId = listID;
    //null check
    if (shoppingListItemId == 0) { return; }

    //Spinner show
    let button = document.getElementById('btnDeleteList');
    button.setAttribute('disabled', 'disabled')
    button.innerHTML = `
            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
            Processing...
            `;

    //based on the provided id, call controller to find async and delete
    let log = await advFetch('/ShoppingList/DeleteShoppingListItem?listID=' + shoppingListItemId, { method: "DELETE" })

    //See result on log
    console.log(log)


    //if error
    if (log.status == 400) {
        console.log(result) // alert
        alert('The selected list already is deleted previously')
        //remove spinner
        button.removeAttribute('disabled');
        button.innerHTML = 'Remove';
        return;

    }
    //if we happy
    else if (log.ok) {
        //remove spinner and reset button
        button.removeAttribute('disabled');
        button.innerHTML = 'Remove';
        //update the DDL
        await updateDDL();
    }
    //force update DDL
    await updateDDL();
    //reload the page
    location.reload();

}

