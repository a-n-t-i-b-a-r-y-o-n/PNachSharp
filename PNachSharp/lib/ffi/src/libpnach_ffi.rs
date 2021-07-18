extern crate libc;

use libpnach::pnach_file;

use libc::c_char;
use std::ffi::{CStr, CString};
use libpnach::pnach_file::PNachFile;

/// PNachFile factory
#[no_mangle]
pub extern "C" fn PNachFile_New(game_title: *const c_char, game_crc: *const c_char) -> *mut pnach_file::PNachFile {
    // Check title pointer for nullness, then unsafely assign
    let title = match game_title.is_null() {
        true => "",
        false => unsafe { CStr::from_ptr(game_title).to_str().unwrap() }
    };
    // Check crc pointer for nullness, then unsafely assign
    let crc = match game_crc.is_null() {
        true => "",
        false => unsafe { CStr::from_ptr(game_crc).to_str().unwrap() }
    };
    // Return raw reference to Boxed PNachFile object
    Box::into_raw(Box::new(pnach_file::PNachFile::new(title, crc)))
}

/// Free PNachFile object from use
#[no_mangle]
pub extern "C" fn PNachFile_Free(pnachfile_ptr: *mut pnach_file::PNachFile) {
    // Do nothing if given a null pointer
    if pnachfile_ptr.is_null() { return }
    // Take Rust ownership of object so that it's freed
    unsafe {
        Box::from_raw(pnachfile_ptr);
    }
}

/// PNachFile.to_string() formatter
#[no_mangle]
pub extern "C" fn PNachFile_ToString(pnachfile_ptr: *mut pnach_file::PNachFile) -> *mut c_char {
    unsafe { CString::new(String::from(&(*pnachfile_ptr).to_string())).unwrap().into_raw() }
}

/// Title accessor
#[no_mangle]
pub extern "C" fn PNachFile_GetTitle(pnachfile_ptr: *mut pnach_file::PNachFile) -> *mut c_char {
    unsafe { CString::new(String::from(&(*pnachfile_ptr).game_title.to_string())).unwrap().into_raw() }
}

/// Title mutator
#[no_mangle]
pub extern "C" fn PNachFile_SetTitle(pnachfile_ptr: *mut pnach_file::PNachFile, title: *const c_char) {
    unsafe { (*pnachfile_ptr).game_title = String::from(CStr::from_ptr(title).to_str().unwrap()); }
}

/// CRC accessor
#[no_mangle]
pub extern "C" fn PNachFile_GetCRC(pnachfile_ptr: *mut pnach_file::PNachFile) -> *mut c_char {
    unsafe { CString::new(String::from(&(*pnachfile_ptr).game_crc.to_string())).unwrap().into_raw() }
}

/// CRC mutator
#[no_mangle]
pub extern "C" fn PNachFile_SetCRC(pnachfile_ptr: *mut pnach_file::PNachFile, crc: *const c_char) {
    unsafe { (*pnachfile_ptr).game_crc = String::from(CStr::from_ptr(crc).to_str().unwrap()); }
}