window.SaveData = function (key, data) {
    this.localStorage.setItem(key, data);
}
window.GetData = function (key) {
    return this.localStorage.getItem(key);
}
window.RemoveData = function (key) {
    this.localStorage.removeItem(key);
}