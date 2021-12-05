
async function CheckMetamaskConnection() {
	// Modern dapp browsers...
	if (window.ethereum) {
		window.web3 = new Web3();
		try {
			// Request account access if needed
			await ethereum.enable();
			// Acccounts now exposed
			return true;
		} catch (error) {
			// User denied account access...
			return false;
		}
	}
	// Legacy dapp browsers...
	else if (window.web3) {
		window.web3 = new Web3(web3.currentProvider);
		// Acccounts always exposed

		return true;
	}
	// Non-dapp browsers...
	else {
		console.log('Non-Ethereum browser detected. You should consider trying MetaMask!');
		return false;
	}
}

$(document).ready(async function () {
	//var IsMetamask = await CheckMetamaskConnection();
	var web3;
	// Modern dapp browsers...
	if (window.ethereum) {
		web3 = new Web3();
		
			// Request account access if needed
			await ethereum.enable();
			// Acccounts now exposed
		
	}
	// Legacy dapp browsers...
	else if (window.web3) {
		web3 = new Web3(web3.currentProvider);
		// Acccounts always exposed

		
	}
	// Non-dapp browsers...
	else {
		console.log('Non-Ethereum browser detected. You should consider trying MetaMask!');
	
	}
	var myContract;

	web3.eth.defaultAccount = web3.eth.accounts[0];
	alert(web3.eth.defaultAccount)
	
	myContract = new web3.eth.Contract([
		{
			"inputs": [],
			"stateMutability": "nonpayable",
			"type": "constructor"
		},
		{
			"anonymous": false,
			"inputs": [
				{
					"indexed": true,
					"internalType": "uint256",
					"name": "_candidateid",
					"type": "uint256"
				}
			],
			"name": "eventVote",
			"type": "event"
		},
		{
			"inputs": [],
			"name": "candidatecount",
			"outputs": [
				{
					"internalType": "uint256",
					"name": "",
					"type": "uint256"
				}
			],
			"stateMutability": "view",
			"type": "function"
		},
		{
			"inputs": [
				{
					"internalType": "uint256",
					"name": "",
					"type": "uint256"
				}
			],
			"name": "candidates",
			"outputs": [
				{
					"internalType": "uint256",
					"name": "id",
					"type": "uint256"
				},
				{
					"internalType": "string",
					"name": "name",
					"type": "string"
				},
				{
					"internalType": "uint256",
					"name": "voteCount",
					"type": "uint256"
				}
			],
			"stateMutability": "view",
			"type": "function"
		},
		{
			"inputs": [
				{
					"internalType": "uint256",
					"name": "_candidateid",
					"type": "uint256"
				}
			],
			"name": "vote",
			"outputs": [],
			"stateMutability": "nonpayable",
			"type": "function"
		},
		{
			"inputs": [
				{
					"internalType": "address",
					"name": "",
					"type": "address"
				}
			],
			"name": "voter",
			"outputs": [
				{
					"internalType": "bool",
					"name": "",
					"type": "bool"
				}
			],
			"stateMutability": "view",
			"type": "function"
		}
	], '0x1f12000ad7fa5c6bc5f41a939849658e18879345');
	
	web3.eth.defaultAccount = '0x77CbfF7bB301506B27d7255Fc7E35bA37ace4A81';
	console.log("gggg")
	console.log(myContract)

	myContract.methods.candidates(1).call( function (err, result) {
		
			console.log("result : ", result);
			document.getElementById("cad" + 1).innerHTML = result[1];
			document.getElementById("cad" + 1 + 'count').innerHTML = result[2].toNumber();
		
	});

	await myContract.eventVote({
		fromBlock: 0
	}, function (err, event) {
		console.log("event :", event);
		getCandidate(event.args._candidateid.toNumber());
	});
	console.log("myContract :", myContract);
	console.log("Metamask detected!")

});


async function getCandidate(cad) {
	await myContract.candidates(cad, function (err, result) {
		if (!err) {
			console.log("result : ", result);
			document.getElementById("cad" + cad).innerHTML = result[1];
			document.getElementById("cad" + cad + 'count').innerHTML = result[2].toNumber();
		}
	});
}

async function Vote(cad) {
	await myContract.vote(cad, function (err, result) {
		if (!err) {
			console.log("We are winning!");
		} else {
			console.log("Can not connect to the smart contract");
		}
	})
}