import { Injectable } from '@angular/core';
import * as CryptoJS from 'crypto-js';

@Injectable()
export class CryptographyService {

  public Encrypt(data: string, key: string): string {
    let keySize = 256;
    let ivSize = 128;
    let iterations = 100;
    //We add salt for encryption
    let salt = CryptoJS.lib.WordArray.random(128 / 8);

    //Adding the salt to the data To increase the security of your master password
    let keyData = CryptoJS.PBKDF2(key, salt, {
        keySize: keySize / 32,
        iterations: iterations
    });
    //An initialization vector (IV) is an arbitrary number that can be used along with a secret key 
    //for data encryption.This number, also called a nonce, is employed only one time in any session.
    let iv = CryptoJS.lib.WordArray.random(128 / 8);

    let encrypted = CryptoJS.AES.encrypt(data, keyData, {
        iv: iv,
        padding: CryptoJS.pad.Pkcs7,
        mode: CryptoJS.mode.CBC
    });

    return salt.toString() + iv.toString() + encrypted.toString();
}

public Decrypt(data: string, key: string): string {
   
    try {
        let keySize = 256;
        let ivSize = 128;
        let iterations = 100;
        let dataStore = data;

        //This condition checks if the string starts and ends with double quotes("")
        //Why we check those quotes because when you get the item in localStorage it always
        //starts and ends with double quotes("") which could lead to invalid format when decrypting 
        //the data
        if (dataStore.substr(data.length - 1, 1) == '"' && dataStore.substr(0, 1) == '"') {
            dataStore = dataStore.slice(0, -1);
            dataStore = dataStore.replace(/^.{0,1}/, '');
        }
        let salt = CryptoJS.enc.Hex.parse(dataStore.substr(0, 32));
        let iv = CryptoJS.enc.Hex.parse(dataStore.substr(32, 32))
        let encrypted = dataStore.substring(64);


        let keyData = CryptoJS.PBKDF2(key, salt, {
            keySize: keySize / 32,
            iterations: iterations
        });

        let decrypted = CryptoJS.AES.decrypt(encrypted, keyData, {
            iv: iv,
            padding: CryptoJS.pad.Pkcs7,
            mode: CryptoJS.mode.CBC
        });
        let descryptedData = decrypted.toString(CryptoJS.enc.Utf8);
        return descryptedData;
    } catch (e) {
        return null;
    }
}

}
