import { Component, Input, Output, EventEmitter } from "@angular/core";
import { Validators, FormGroup, FormControl } from "@angular/forms";
import { FileRestrictions, SelectEvent } from "@progress/kendo-angular-upload";
import { Book } from "../Model/Book";

@Component({
  selector: 'app-edit-book',
  templateUrl: './edit-book.component.html',
  styleUrls: ['./edit-book.component.css']
})
export class EditBookComponent {
  public active = false;
  public minNumber = 0;
  public editForm: FormGroup = new FormGroup({
    id: new FormControl(0),
    title: new FormControl(new Date(), Validators.required),
    description: new FormControl("", Validators.required),
    summary: new FormControl("", Validators.required),
    isbn: new FormControl("", Validators.required),
    author: new FormControl("", Validators.required),
    image: new FormControl(null),
    year: new FormControl(new Date(), Validators.required),
    price: new FormControl("", Validators.required),
    numPages: new FormControl("", Validators.required),
    quantity: new FormControl("", Validators.required),
    numPurchases: new FormControl("", Validators.required)
  });
  public fileRestrictions: FileRestrictions = {
    allowedExtensions: [".jpg", ".png"],
    maxFileSize: 10485760,
  };
  @Input() public isNew?: boolean;
  @Input() public set model(book: Book | undefined) {
    if (book != undefined) {
      book.year = new Date(book.year);
    }

    this.editForm.reset(book);
    this.active = book !== undefined;
  }
  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<Book> = new EventEmitter();

  constructor() { }

  public onSave(e: any): void {
    e.preventDefault();
    this.save.emit(this.editForm.value);
    this.active = false;
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.closeForm();
  }

  public closeForm(): void {
    this.active = false;
    this.cancel.emit();
  }

  public imageSelectEventHandler(e: SelectEvent): void {

    let reader = new FileReader();

    // Setup onload event for reader
    reader.onload = () => {
      if (reader.result != null) {
        // Store base64 encoded representation of file
        this.editForm.patchValue({ image: reader.result });
        console.log(this.editForm.get("image"))
      }
    }

    // Read the file
    if (e.files[0].rawFile != undefined)
      reader.readAsDataURL(e.files[0].rawFile);

  }
}
