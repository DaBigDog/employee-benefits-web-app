"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (Object.prototype.hasOwnProperty.call(b, p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
exports.DeductionCosts = exports.Employee = exports.Dependent = exports.Person = void 0;
var Person = /** @class */ (function () {
    function Person() {
        this.id = 0;
    }
    return Person;
}());
exports.Person = Person;
var Dependent = /** @class */ (function (_super) {
    __extends(Dependent, _super);
    function Dependent() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return Dependent;
}(Person));
exports.Dependent = Dependent;
var Employee = /** @class */ (function (_super) {
    __extends(Employee, _super);
    function Employee() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.dependents = new Array();
        return _this;
    }
    return Employee;
}(Person));
exports.Employee = Employee;
var DeductionCosts = /** @class */ (function () {
    function DeductionCosts() {
    }
    return DeductionCosts;
}());
exports.DeductionCosts = DeductionCosts;
//# sourceMappingURL=employee-list.model.js.map