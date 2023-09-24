﻿using System;


namespace EventBus.Messages.Events
{
    public class IntegrationBaseEvent
    {
         
        public Guid Id { get; private set; }
        public DateTime CreateDate { get; private set; }

        public IntegrationBaseEvent()
        {
            Id = Guid.NewGuid();
            CreateDate = DateTime.UtcNow;
        }
        public IntegrationBaseEvent(Guid id,DateTime createDate)
        {
            Id = id;
            CreateDate = createDate;
        }
    }
}