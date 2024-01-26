import { useState, useEffect } from "react"
import Delete from "./Delete"
import Edit from "./Edit"
import New from "./New"
import {testData} from "./Lib"
import Appointment from "./Appointment"

export default function Home(props)
{
  const [dataList, setDataList] = useState([])

  useEffect(()=>{
    setDataList(testData)
  },[])

  return (
    <main>

      <h1>
        Manage your Appointments / Dates very easy
      </h1>

      <p>This powerful web application helps you to mamage your dates very easy.</p>

      <div className="add-btn disp items-center">

        <div className="btn add">+</div>

      </div>

      <div className="notifications spacer-20">Some text!</div>
      
      <section className="disp justity-spc-btw filter">

        <div className="modal-title">Filter</div>

        <div className="disp items-center filter-items">

          <button className="me-15">Clear Filters</button>

          <div>
            
            <label htmlFor="All_f">All</label> <br/>

            <input type="checkbox" id="All_f" name="All"/>
          
          </div>

          <div>
            <label htmlFor="Done_f">Done</label> <br/>

            <input type="checkbox" id="Done_f" name="All"/>
          </div>

          <div>
            <label htmlFor="Deleted_f">Deleted</label> <br/>

            <input type="checkbox" id="Deleted_f" name="All"/>
          </div>

          <div>
            <label htmlFor="period">Period</label> <br/>
            
            <select name="period" id="period" defaultValue={"4"}>
              <option value="5" disabled>Period</option>
              <option value="4">Default</option>
              <option value="1">Today</option>
              <option value="2">This week</option>
              <option value="3">Last week</option>
            </select>
          
          </div>

          <div>
            <label htmlFor="SpecifiedDate">Specified Date</label> <br/>
          
            <input type="date" id="SpecifiedDate" name="SpecifiedDate"/>
          </div>

          <div>
            <label htmlFor="SpecifiedTime">Specified Time</label> <br/>
            
            <input type="time" id="SpecifiedTime" name="SpecifiedTime"/>
          </div>

          <div>
            <label htmlFor="LevelOfImportance_f">Level Of Importance</label> <br/>
            
            <select name="LevelOfImportance" id="LevelOfImportance_f" defaultValue={8}>
              <option value={8} disabled>Level Of Importance</option>
              <option value={9}>Reset</option>
              <option value={5}>Very High</option>
              <option value={4}>High</option>
              <option value={3}>Medium</option>
              <option value={2}>Normal</option>
              <option value={1}>Low</option>
              <option value={0}>Very Low</option>
            
            </select>
          </div>

        </div>

      </section>

      <div className="disp underline hdr">
        <div className="column id">#</div>
        <div className="column title">Title</div>
        <div className="column description">Description</div>
        <div className="column importance">Importance</div>
        <div className="column date">Date</div>
        <div className="column time">Time</div>
        <div className="column addr">Address</div>
        <div className="column edit">Edit</div>
        <div className="column delete">Delete</div>
        
      </div>

      
      { dataList.length === 0 ? 
        <div className="disp mt-15 waiting">Loading<div className="loading">...</div></div> :
        dataList.map(item => <Appointment item={item} key={item.id}/>)
      }

      <section>
        <section className="model new-model">
          <New/>
        </section>

        <section className="model edit-model hidden">
          <Edit/>
        </section>
        
        <section className="model delete-model hidden">
          <Delete/>
        </section>
        
      </section>
      
    </main>
  )
}